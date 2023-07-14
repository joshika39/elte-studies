import random

def swap(a, b):
    temp = a
    a = b
    b = temp


def partition(A: list, p: int, r: int, debug=False):
    if debug:
        i = int(input("Enter the pivot: "))
    else:
        i = random.randint(p, r)
    x = A[i]
    A[i] = A[r]
    i = p
    while i < r and A[i] <= x:
        i += 1
    if i < r:
        j = i + 1
        while j < r:
            if A[j] < x:
                A[i], A[j] = A[j], A[i]
                i += 1
            j += 1
        A[r] = A[i]
        A[i] = x
    else:
        A[r] = x
    return i

def quicksort(A: list, p: int, r: int, debug=False):
    if p < r:
        q = partition(A, p ,r, debug)
        quicksort(A, p, q - 1, debug)
        quicksort(A, q + 1, r, debug)

def sort(target: list, debug=False):
    quicksort(target, 0, len(target), debug)
    return target

if __name__ == "__main__":
    l = [4,3,5,8,2,4,1,7,6]
    quicksort(l, 0, len(l) - 1)