class E2:
    
    def __init__(self, val) -> None:
        self.key = val
        self.prev = self
        self.next = self


def unlink(q: E2):
    p = q.prev
    r = q.next
    p.next = r
    r.prev = p
    q.next = q
    q.prev = q

def preceed(q: E2, r: E2):
    pass

def follow(p: E2, q: E2):
    pass

def splice(p: E2, q: E2, r: E2):
    p1 = p.prev
    q2 = q.next
    p1.next = q2
    q2.prev = p1
    p1 = r.prev
    p.prev = p1
    q.next = r
    p1.next = p
    r.prev = q

def append(L: E2, H: E2):
    if H.next != H:
        splice(H.next, H.prev, L)

def instertionSort(H: E2):
    r = H.next
    s = r.next
    while s != H:
        if r.key <= s.key:
            r = s
        else:
            unlink(s)
            p = r.prev
            while p != H and p.key > s.key:
                p = p.prev
            follow(p, s)
        s = r.next
