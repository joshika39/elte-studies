module Homework2 where

isSmallPrime :: Int -> Bool
isSmallPrime 5 = True
isSmallPrime 7 = True
isSmallPrime 11 = True
isSmallPrime 13 = True
isSmallPrime _ = False

equivalent :: Bool -> Bool -> Bool
equivalent True True = True
equivalent True False = False
equivalent False True = False
equivalent False False = True

xor :: Bool -> Bool -> Bool
xor True True = False
xor False True = True
xor True False = True
xor False False = False


invertY :: (Int, Int) -> (Int, Int)
invertY (x, y) = (x, negate y)

isOnNeg4X :: (Int, Int) -> Bool
isOnNeg4X (x, y) = y == (-4) * x

yDistance :: (Int, Int) -> (Int, Int) -> Int
yDistance (x1, y1) (x2, y2) = abs (y1 - y2)

add :: (Int, Int) -> (Int, Int) -> (Int, Int)
add (x1, y1) (x2, y2) = (x1 * y2 + x2 * y1, y1 * y2)

multiply :: (Int, Int) -> (Int, Int) -> (Int, Int)
multiply (x1, y1) (x2, y2) = (x1 * x2, y1 * y2)

divide :: (Int, Int) -> (Int, Int) -> (Int, Int)
divide (x1, y1) (x2, y2) = (x1 * y2, x2 * y1)
