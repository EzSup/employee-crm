import { MagnifyingGlass } from "@phosphor-icons/react";
import { FC } from "react";
import "../../styles/components/employeesList/SearchBar.scss";

interface SearchBarProps {
	placeholder: string;
	onValueUpdate: (value: string) => void;
}

const SearchBar: FC<SearchBarProps> = (props: SearchBarProps) => {
	return (
		<div className="search-component">
			<input
				placeholder={props.placeholder}
				onChange={(e) => {
					props.onValueUpdate(e.target.value);
				}}
			/>
			<button>
				<MagnifyingGlass size={16} />
			</button>
		</div>
	);
};

export default SearchBar;
