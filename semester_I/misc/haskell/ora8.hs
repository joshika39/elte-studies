any2 :: (a -> Bool) -> [a] -> Bool
-- any2 p xs = length (filter p xs) /= 0
-- any2 p xs = null (filter p xs)
any2 p xs = or (map p xs)

iterate' :: (a -> a) -> a -> [a]
iterate' f x = x : iterate' f (f x)
