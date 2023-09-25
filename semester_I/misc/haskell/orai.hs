module Orai1 where

-- signum' :: (Ord a, Num a) => -


props3 xs n =
  let
    nth xs n = head (drop n xs) 
    v = nth xs n 
  in (v, v ^ 2, even v)
