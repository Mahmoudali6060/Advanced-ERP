export class Node {
	id: number;
	name: string;
	checked: boolean;
	categoryId: number;
	pageId: number;
	level: number;
	children: Array<Node>;
}
