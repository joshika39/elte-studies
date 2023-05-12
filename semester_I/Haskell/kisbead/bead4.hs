heads :: [a] -> [b] -> (a,b)
heads x y = (x1, y1) where
    y1 = head y
    x1 = head x

duplicate :: [a] -> [a]
duplicate (a:b:al) = (a:a:b:b:(al))

replaceHead :: [a] -> [a] -> [a]
replaceHead (x:xs) (y:ys) = (y:xs)

partialNth :: [a] -> Integer -> a
partialNth (a1:as) 0 = a1
partialNth (a1:a2:as) 1 = a2
partialNth (_:_:a3:as) 2 = a3

initials :: ([Char],[Char],[Char]) -> [Char]
initials ((a1:as), (b1:bs), (c:cs)) = a1:b1:[c]

joinFirstTwo :: [(a,Integer,a)] -> (a,Integer,a)
joinFirstTwo ((a1,a2,a3) : (b1,b2,b3) : c) = (a1, max b2 a2, b3)

mix :: [[a]] -> [a]
mix ((a1:a2:as) : (b1:b2:bs):c) = [a2,b1]