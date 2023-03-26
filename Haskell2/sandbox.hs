foo [] = []
foo ((5, x): xs) = (5 + 1, x) : xs