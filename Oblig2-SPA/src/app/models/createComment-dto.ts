export interface CreateCommentDto {
    content: string;
    parentCommentId: number | null;
}
