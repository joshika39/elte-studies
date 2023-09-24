def is_variable(value) -> bool:
	''' 
	A function that returns if the value of the Node is a math variable or not
	:return bool:
	'''
	if (value >= 'a' and value <= 'z') or (value >= 'A' and value <= 'Z'):
		return True
	else:
		return False

class Node():
	key = None

	def __init__(self, key):
		self.key = key
		self.left = None  #type: Node
		self.right = None  #type: Node

	@classmethod
	def create(cls, x):
		return cls(x)
	

	@classmethod
	def create_empty(cls):
		return cls(None)

class BaseTree():
	main_root = None  #type: Node
	
	def brackets(self, root: Node) -> str:
		if root is not None:
			if root.left is None and root.right is None:
				return f'{root.key}'
			else:
				if root.left is not None and is_variable(root.left.key):
					left_str = self.brackets(root.left)
				else:
					left_str = f'({self.brackets(root.left)})'

				if root.right is not None and is_variable(root.right.key):
					right_str = self.brackets(root.right)
				else:
					right_str = f'({self.brackets(root.right)})'

			return (f'{left_str} {root.key} {right_str}')
		else:
			return ""
	
	def inorder(self, root: Node):
		if root:
			self.inorder(root.left)
			print(root.key, end=' ')
			self.inorder(root.right)

	def print_tree(self, root: Node, level=0):
		if root is not None:
			self.print_tree(root.right, level=level+1)
			print('     ' * level, end='')
			print(f'{root.key}')
			self.print_tree(root.left, level=level+1)
		
	def lowest_level(self, root: Node) -> int:
		if not root:
			return -1
		
		if not root.left and not root.right:
			return 0
		ll = self.lowest_level(root.left)
		lr = self.lowest_level(root.right)
		return min(ll, lr) + 1

class BinaryTree(BaseTree):
	main_root = None  #type: Node

	def add(self, new_value, root=main_root):
		if root is None:
			return Node.create(new_value)
		else:
			if new_value < root.key:
				root.left = self.add(new_value, root.left)
			else:
				root.right = self.add(new_value, root.right)
			return root