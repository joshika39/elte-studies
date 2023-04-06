module Homework1 where

intExpr1 :: Int
intExpr1 = 4

intExpr2 :: Integer
intExpr2 = 45

intExpr3 :: Int
intExpr3 = 65

charExpr1 :: Char
charExpr1 = 'a'

charExpr2 :: Char
charExpr2 = 'b'

charExpr3 :: Char
charExpr3 = 'c'

boolExpr1 :: Bool
boolExpr1 = True

boolExpr2 :: Bool
boolExpr2 = False

boolExpr3 :: Bool
boolExpr3 = True -- Boolean V2, ez egy nagyon specialis 'True' ami teljes mertekben nem egyezik meg a 21 sorban levovel😂

remainingSeeds :: Int
remainingSeeds = mod 183 13

canPlantAll :: Bool
canPlantAll = mod 183 13 == 0

inc :: Int -> Int
inc a = a + 1

double :: Int -> Int
double a = a * 2

seven1 :: Int
seven1 = double 3 + inc 0

seven2 :: Int
seven2 = double 2 + inc 2

seven3 :: Int
seven3 = inc 6 + double 0

cmpRem5Rem7 :: Int -> Bool
cmpRem5Rem7 a = (mod a 5) > (mod a 7)

foo :: Int -> Bool -> Bool
foo a b
  | b == True = odd a
  | otherwise = even a

bar :: Bool -> Int -> Bool
bar a b = foo b a

greaterThenTwo :: Int -> Bool
greaterThenTwo a = (mod a 5) > 2

volume :: Integer -> Integer -> Integer -> Integer
volume a b c = a * b * c