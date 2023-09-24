max_sum = 0
current_sum = 0
start_i = 1
temp_start_i = 1
end_i = 1

A = ["0", 1, -5, 0, 6, 9, 8, -2, 4]
for i in range(1, len(A)):
	print(f'Adding: ({A[i]})')
	current_sum += A[i]
	if current_sum < 0:
		current_sum = 0
		temp_start_i = i + 1
	if current_sum > max_sum:
		max_sum = current_sum
		start_i = temp_start_i
		end_i = i

print(f"{max_sum}, start: {start_i}, end: {end_i}")