import { UserDto } from "./user-dto";

export interface CommentDto {
    id: number;
    content: string;
    createdBy: UserDto | null;
    discussionId: number | null;
    parentCommentId: number | null;
    replies: CommentDto[];
}
