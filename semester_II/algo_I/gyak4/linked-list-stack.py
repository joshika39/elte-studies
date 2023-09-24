class Node:
    def __init__(self, val):
        self.val = val
        self.next = None  #type: Node
  
  
class Stack:
    def __init__(self):
        self.head = None
  
    def isempty(self):
        if self.head == None:
            return True
        else:
            return False
  
    def push(self, data):
  
        if self.head == None:
            self.head = Node(data)
  
        else:
            newnode = Node(data)
            newnode.next = self.head
            self.head = newnode
  
    def pop(self):
        if not self.isempty():
            poppednode = self.head
            self.head = self.head.next
            poppednode.next = None
            return poppednode.val
  
    def top(self):
        if not self.isempty():
            return self.head.val