from bintree import Node, BaseTree

# def generate_tree(elements: list) -> Node:
# 	tree = None
# 	for e in elements:
# 		tree = add_to_tree(tree, e)
# 	return tree


# t = generate_tree("+*/b-ac^b2d")

# t = Node(1)
# t.left = Node(2)
# t.left.left = Node(3)
# t.left.left.right = Node(4)
# t.left.left.right.left = Node(9)
# t.right = Node(7)
# t.right.right = Node(5)
# t.right.right.left = Node(8)
# t.right.right.left.right = Node(2)
# t.right.right.left.left = Node(6)
bT = BaseTree()

bT.main_root = Node('+')
bT.main_root.right = Node('*')
bT.main_root.right.right = Node('d')
bT.main_root.right.left = Node('^')
bT.main_root.right.left.left = Node('b')
bT.main_root.right.left.right = Node('2')
bT.main_root.left = Node('/')
bT.main_root.left.left = Node('b')
bT.main_root.left.right = Node('-')
bT.main_root.left.right.right = Node('c')
bT.main_root.left.right.left = Node('a')

# t = Node(3)
# t.left = Node(2)
# t.left.left = Node(1)
# t.right = Node(6)
# t.right.left = Node(4)
# t.right.left.right = Node(5)
# t.right.right = Node(7)

# inorder_traversal(t)
# print_tree(t)

print(f'({bT.brackets(bT.main_root).replace(" ", "")})')

print(bT.lowest_level(bT.main_root))

