import { CommentDto } from "./comment-dto";
import { UserDto } from "./user-dto";

export interface DiscussionDto {
    id: number;
    title: string;
    content: string;
    createdBy: UserDto | null;
    comments: CommentDto[];
}
