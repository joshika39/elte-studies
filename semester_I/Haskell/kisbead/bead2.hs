--isTripleOdd :: Integer -> Bool
isTripleOdd x = odd (3*x)

--max3 :: Integer -> Integer -> Integer -> Integer
max3 a b c = max (max a b) c

--pancake :: Integer -> Integer -> Integer
pancake n k = div k n

--beer :: Integer -> Integer -> Integer -> Integer
beer n m k = div (mod k n) m

foo :: Integer -> Bool -> Bool
foo a b = 
  if b == True then
    odd a
  else
    even a

bar :: Bool -> Integer -> Bool
bar a b = 
  if a == True then
    even 2
  else
    odd 2

