module Ora1 where
{- beginnig of the first Haskell class
-  
this is how you write a long comment-}

-- definition equality
one :: Integer
one = 1

sumNum n = 
  if n == 0 
  then 0
  else n + sumNum (n-1)

sumGuardian n 
  | n == 0 = 0
  | otherwise = n + sumGuardian (n-1)

digitAdd num
  | num == 0 = 0
  | otherwise = digitAdd num / 10
  where
    digit = mod num 10
