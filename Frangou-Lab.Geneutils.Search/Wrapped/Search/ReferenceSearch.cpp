/*
* Copyright 2018 Frangou Lab
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*    http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

#include "ReferenceSearch.h"
#include "../Managed.h"
#include "../../../libgene/source/operations/filter/Filter.hpp"

#pragma managed

namespace FrangouLab::Geneutils::Search
{
	using namespace System;
	using namespace System::Runtime::InteropServices;

	void ReferenceSearch::Search(ReferenceSettings ^ settings)
	{
		Search(settings, nullptr);
	}

	void ReferenceSearch::Search(ReferenceSettings ^ settings, Predicate<Single>^ progress)
	{
		if (settings == nullptr)
			throw gcnew System::ArgumentNullException("settings");

		if (!settings->IsValid())
			throw gcnew System::ApplicationException("Settings invalid");

		auto input = Managed::ToString(settings -> Input);
		auto output = Managed::ToString(settings -> Output);
		auto primers = GetPrimers(settings);

		try
		{
			auto fiter = std::make_unique<Filter>(input, output, primers);

			fiter->updateProgress = ProgressBridgeFor(progress);
			fiter->Process();
		}
		catch (const std::exception& exception)
		{
			throw Managed::ToManagedApplicationException(exception);
		}
	}

	std::vector<SearchPrimer> ReferenceSearch::GetPrimers(ReferenceSettings ^ settings)
	{
		bool dashObserved = false;

		std::vector<SearchPrimer> primers;
		std::vector<std::string> stdQueries = Managed::ToArray(settings -> Queries);

		for (int64_t i = 0; i < stdQueries.size(); ++i)
		{
			const auto& item = stdQueries[i];
			if (settings -> IsOnlyMixedStrainPrimers && item == "-")
			{
				dashObserved = true;
				stdQueries.erase(std::begin(stdQueries) + i);
				--i;
				continue;
			}

			int64_t commaPosition = item.find(',');
			bool idIsProvided = (commaPosition != std::string::npos);
			if (idIsProvided)
			{
				auto queryId = item.substr(0, commaPosition);
				auto query = item.substr(commaPosition + 1);

				if (settings->IsOnlyMixedStrainPrimers)
					primers.emplace_back(SearchPrimer { queryId, query, settings->isRnaInput, settings->IsRnaPrimers, !dashObserved });
				else
					primers.emplace_back(SearchPrimer { queryId, query, settings->isRnaInput, settings->IsRnaPrimers });
			}
			else
			{
				if (settings->IsOnlyMixedStrainPrimers)
					primers.emplace_back(SearchPrimer(item, (int) i + 1, settings->isRnaInput, settings->IsRnaPrimers, !dashObserved));
				else
					primers.emplace_back(SearchPrimer(item, (int) i + 1, settings->isRnaInput, settings->IsRnaPrimers));
			}
		}

		return primers;
	}
}
