import { FC, useState } from "react";
import "../../styles/components/employeesList/TableCustomization.scss";
import {
	EnglishLevel,
	Gender,
	PersonFilters,
	personFiltersDefaultValue,
	TShirtSize,
} from "../../models/employeeDataModels";
import FiltrationSection from "./FiltrationSection";

interface TableFiltrationProps {
	filters: PersonFilters;
	onFiltersChange: (filters: PersonFilters) => void;
}

const TableFiltrationWindow: FC<TableFiltrationProps> = ({
	filters,
	onFiltersChange,
}: TableFiltrationProps) => {
	const clearFilters = () => {
		onFiltersChange(personFiltersDefaultValue());
	};

	const [openSectionId, setOpenSectionId] = useState<string | null>(null);

	const handleSectionToggle = (sectionId: string) => {
		setOpenSectionId(openSectionId === sectionId ? null : sectionId);
	};

	return (
		<div className="field-selector" id="filtration-window">
			<h3>Filtration</h3>
			<div className="fields-container">
				{filters.gender && (
					<FiltrationSection
						title="Gender"
						values={getEnumValues(Gender)}
						enumType={Gender}
						selectedValues={filters.gender || []}
						onValueChange={createFilterHandler(
							filters,
							"gender",
							onFiltersChange
						)}
						isOpen={openSectionId === "gender"}
						onToggle={handleSectionToggle}
						sectionId="gender"
					/>
				)}
				{filters.englishLevel && (
					<FiltrationSection
						title="English level"
						values={getEnumValues(EnglishLevel)}
						enumType={EnglishLevel}
						selectedValues={filters.englishLevel || []}
						onValueChange={createFilterHandler(
							filters,
							"englishLevel",
							onFiltersChange
						)}
						isOpen={openSectionId === "englishLevel"}
						onToggle={handleSectionToggle}
						sectionId="englishLevel"
					/>
				)}
				{filters.tShirtSize && (
					<FiltrationSection
						title="T-Shirt size"
						values={getEnumValues(TShirtSize)}
						enumType={TShirtSize}
						selectedValues={filters.tShirtSize || []}
						onValueChange={createFilterHandler(
							filters,
							"tShirtSize",
							onFiltersChange
						)}
						isOpen={openSectionId === "tShirtSize"}
						onToggle={handleSectionToggle}
						sectionId="tShirtSize"
					/>
				)}
			</div>
			<button className="primary-button" onClick={clearFilters}>
				Clear filters
			</button>
		</div>
	);
};

function getEnumValues<T>(enumObject: { [key: string]: string | number }): T[] {
	return Object.keys(enumObject)
		.filter((key) => !isNaN(Number(key)))
		.map((key) => Number(key) as T);
}

function createFilterHandler<K extends keyof PersonFilters>(
	filters: PersonFilters,
	filterKey: K,
	onFiltersChange: (filters: PersonFilters) => void
) {
	return (selectedValues: PersonFilters[K]) => {
		onFiltersChange({
			...filters,
			[filterKey]: selectedValues,
		});
	};
}

export default TableFiltrationWindow;
