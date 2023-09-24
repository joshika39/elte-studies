class Node:
    def __init__(self, data=None):
        self.data = data
        self.next = None  #type: Node
        self.prev = None  #type: Node


def add_to_list(head: Node, value):
    if head is None:
        return Node(value)
    
    while head.next is not None:
        head = head.next

    if head.data is None:
        head.data = value
    else:
        head.next = Node(value)
        head.next.prev = head


def get_head(node: Node):
    while node.prev is not None:
        node = node.prev

    return node


def get_tail(node: Node):
    while node.next is not None:
        node = node.next
    return node


def combine(node1: Node, node2: Node):
    if node1 is None:
        return node2
    
    tail = get_tail(node1)
    tail.next = node2 
    node2.prev = tail

    return node2

def distribution_sort(head: Node, digit: int, r=10) -> Node:
    b = []  #type: list[Node]
    for _ in range(r):
        b.append(Node())
    
    while head is not None:
        add_to_list(b[int(head.data[digit])], head.data)
        head = head.next

    result = None
    for i in range(r-1, -1, -1):
        if b[i].data is not None:
            result = combine(result, b[i])

    return get_head(result)


def radix_sort(head: Node, longest: int):
    for i in range(longest - 1, -1, -1):
        head = distribution_sort(head, i)

    return head


def print_list(head) -> str:
    res = ""
    current = head
    while current is not None:
        res += f"{current.data} "
        current = current.next
    return res


def create_list(numbers: list[int]):
    longest = len(str(max(numbers)))
    print(f"Longest number: {longest}")
    node = None
    for i in range(0, len(numbers)):
        n = str(numbers[i])
        abs_len = abs(longest - len(n))
        f_n = f"{'0'*abs_len}{n}"
        if node is None:
            node = Node(f_n)
        else:
            add_to_list(node, f_n)
    return get_head(node), longest


head, l = create_list([170, 45, 75, 90, 802, 24, 2, 66])
print("Eredeti lista:")
print(print_list(head))

head = radix_sort(head, l)
print(print_list(head))