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

#include "GeneralSearch.h"
#include "..\Managed.h"

#pragma managed

namespace FrangouLab::Geneutils::Search
{
	using namespace System;
	using namespace System::Runtime::InteropServices;

	void GeneralSearch::Search(GeneralSettings ^ settings) 
	{
		Search(settings, nullptr);
	}

	void GeneralSearch::Search(GeneralSettings ^ settings, Predicate<Single>^ progress)
	{
		if (settings == nullptr)
			throw gcnew System::ArgumentNullException("settings");

		if (!settings -> IsValid())
			throw gcnew System::ApplicationException("Settings invalid");

		auto input = Managed::ToArray(settings -> Input);
		auto output = Managed::ToString(settings -> Output);
		auto queries = Managed::ToArray(settings -> Queries);
		auto flags = ToFlags(settings->Parameters);

		try 
		{
			auto filter = std::make_unique<Finder>(input, output, std::move(flags), queries);
			
			filter->update_progress_callback = ProgressBridgeFor(progress); 
			filter->Process();
		}
		catch (const std::exception& exception)
		{
			throw Managed::ToManagedApplicationException(exception);
		}
	}

	std::unique_ptr<CommandLineFlags> GeneralSearch::ToFlags(IDictionary<String^, String^>^ parameters)
	{
		auto flags = std::make_unique<CommandLineFlags>();

		for each (auto var in parameters)
		{
			auto key = Managed::ToString(var.Key);
			auto value = Managed::ToString(var.Value);

			flags->SetSetting(key, value);
		}

		return std::move(flags);
	}
}