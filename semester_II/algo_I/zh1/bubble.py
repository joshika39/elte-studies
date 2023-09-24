def bubblesort(nums: list[int | float], debug=False) -> list[int | float]:
    n = len(nums)
    for i in range(n, 1, -1):
        for j in range(0, i-1):
            # if debug:
                # print(f'Checking: {nums[j]} > {nums[j+1]}?')
            if nums[j]>nums[j+1]:
                nums[j], nums[j+1] = nums[j+1], nums[j]
        if debug:
            print(nums)
    return nums

def bubblesort_i(nums: list[int | float], debug=False) -> list[int | float]:
    i = len(nums)
    while i >= 2:
        u = 0

        for j in range(0, i - 1):
            if nums[j] > nums[j+1]:
                nums[j], nums[j+1] = nums[j+1], nums[j]
                u = j
                print(f"u: {u}, ", end='')
        print()
        if debug:
            print(f"{nums}, u: {u}")
        i = u
    return nums

print(bubblesort_i([12, 3, 5, 6, 1], True))