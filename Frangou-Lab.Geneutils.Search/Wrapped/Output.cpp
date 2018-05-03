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

#include "Output.h"
#include "Managed.h"
#include "../../../libgene/source/log/Logger.hpp"

#include <iostream>

#pragma managed

namespace FrangouLab::Geneutils::Search
{
	using namespace System;
	using namespace System::Runtime::InteropServices;

	delegate void OutputDelegate(std::string);

	static Output::Output()
	{
		auto fp = gcnew OutputDelegate(&Output::RaiseLogEvent);
		auto gch = GCHandle::Alloc(fp);
		auto ip = Marshal::GetFunctionPointerForDelegate(fp);
		auto lambda = static_cast<void(*)(std::string)>(ip.ToPointer());

		GC::Collect();

		logger::logLambda = lambda;
	}

	void Output::RaiseLogEvent(std::string log)
	{
		auto message = Managed::ToString(log);
		Log(message);
	}
}
