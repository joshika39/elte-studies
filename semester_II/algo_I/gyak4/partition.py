class Node:
    def __init__(self, val=0, next:'Node'=None):
        self.val = val
        self.next = next

    def __str__(self):
            return f'{self.val} -> {self.next}'

def partition(head: Node) -> Node:
    if not head or not head.next:
        return head
    
    smaller = Node()
    larger = Node()
    
    cur = head
    small_cur = smaller
    large_cur = larger
    
    while cur:
        if cur.val < head.val:
            small_cur.next = cur
            small_cur = cur
        else:
            large_cur.next = cur
            large_cur = cur
        cur = cur.next
    
    small_cur.next = larger.next
    large_cur.next = None
    
    return smaller.next

e1 = Node(4)
e2 = Node(3)
e3 = Node(5)
e4 = Node(9)
e5 = Node(2)
e6 = Node(7)
e7 = Node(1)


e1.next = e2
e2.next = e3
e3.next = e4
e4.next = e5
e5.next = e6
e6.next = e7

print(e1)

print(partition(e1))



