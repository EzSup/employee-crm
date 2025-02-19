import { FC } from "react";
import Avatar from "react-avatar";

interface UserProfileProps {
	photo?: string;
	title: string;
	subtitle: string;
}

const UserProfile: FC<UserProfileProps> = ({ photo, title, subtitle }) => {
	return (
		<div className="user-cell">
			<Avatar
				className="profilePic"
				size="50"
				round={true}
				name={title}
				src={photo}
				textSizeRatio={2}
			/>
			<div className="names-labels">
				<span className="main-line">{title}</span>
				<span className="secondary-line">{subtitle}</span>
			</div>
		</div>
	);
};

export default UserProfile;
