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

#pragma once

#include "../../../libgene/source/def/Flags.hpp"

#include "Managed.h"

namespace FrangouLab::Geneutils::Search
{
	using namespace System;

	public ref class SearchFlags
	{
	public:
		static const String ^ kRnaSequenceSearch = Managed::ToString(Flags::kRnaSequenceSearch);
		static const String ^ kRnaPrimersSearch = Managed::ToString(Flags::kRnaPrimersSearch);

		static const String ^ kForwardAndReverseComplementsSearch = Managed::ToString(Flags::kForwardAndReverseComplementsSearch);
		static const String ^ kMismatchesEnabled = Managed::ToString(Flags::kMismatchesEnabled);

		static const String ^ kContextEnabled = Managed::ToString(Flags::kContextEnabled);
		static const String ^ kMaxAmpliconSize = Managed::ToString(Flags::kMaxAmpliconSize);
		static const String ^ kMinAmpliconSize = Managed::ToString(Flags::kMinAmpliconSize);

		static const String ^ kCoupledQueries = Managed::ToString(Flags::kCoupledQueries);
		static const String ^ kPairedQueryExtraction = Managed::ToString(Flags::kPairedQueryExtraction);
		static const String ^ kMixedStrainPairedPrimerSearch = Managed::ToString(Flags::kMixedStrainPairedPrimerSearch);
		static const String ^ kSearchBindingTargets = Managed::ToString(Flags::kSearchBindingTargets);

		static const String ^ kInputFormat = Managed::ToString(Flags::kInputFormat);
	};
}

