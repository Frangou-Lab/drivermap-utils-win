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

#include "Search.h"

#pragma managed

namespace FrangouLab::Geneutils::Search
{
	using namespace System;
	using namespace System::Runtime::InteropServices;

	std::function<bool(float)> Search::ProgressBridgeFor(Predicate<Single> ^ progress)
	{
		if (progress == nullptr)
			return std::function<bool(float)>();

		_progress = progress;
		_progressDelegate = gcnew ProgressDelegate(this, &Search::Progress);
		_progressPointer = Marshal::GetFunctionPointerForDelegate(_progressDelegate);

		auto cptr = _progressPointer.ToPointer();
		return static_cast<bool(*)(float)>(cptr);
	}

	bool Search::Progress(float progress)
	{
		return _progress(progress);
	}
}

