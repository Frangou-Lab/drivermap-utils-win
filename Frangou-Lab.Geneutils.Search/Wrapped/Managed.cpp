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

#include "Managed.h"
#include <msclr/marshal_cppstd.h>

namespace FrangouLab::Geneutils::Search
{
	String ^ Managed::ToString(std::string string)
	{
		return gcnew String(string.c_str());
	}

	std::string Managed::ToString(String^ string)
	{
		return msclr::interop::marshal_as<std::string>(string);
	}

	array<String^>^ Managed::ToArray(std::vector<std::string> vector)
	{
		int size = vector.size();
		auto strarray = gcnew array<String^>(size);

		for (int i = 0; i < size; i++) {
			strarray[i] = ToString(vector[i]);
		}

		return strarray;
	}

	std::vector<std::string> Managed::ToArray(array<String^>^ arr)
	{
		int length = arr->Length;
		std::vector<std::string> vector(length);

		for (int i = 0; i<length; i++) {
			vector[i] = Managed::ToString(arr[i]);
		}

		return vector;
	}

	ApplicationException ^ Managed::ToManagedApplicationException(const std::exception & exception)
	{
		auto what = exception.what();
		auto message = Managed::ToString(what);

		return gcnew System::ApplicationException(message);
	}
}


