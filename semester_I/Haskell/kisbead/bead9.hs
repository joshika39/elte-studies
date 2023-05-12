import Data.List
data Symbol = X | O | E deriving (Eq, Show)
data Step = St Symbol Int Int deriving (Eq, Show)  
data Board = Bd [[Symbol]] deriving (Eq, Show)

unBoard :: Board -> [[Symbol]]
unBoard (Bd bd) = bd

plain :: Symbol -> Int -> Board
plain s n = Bd (replicate n (replicate n s))

readSymbol :: Board -> Int -> Int -> Symbol
readSymbol (Bd bd) col row = (bd !! col) !! row

mark' :: [[a]] -> a -> (Int, Int) -> [[a]]
mark' m x (r,c) = take r m ++ [take c (m !! r) ++ [x] ++ drop (c + 1) (m !! r)] ++ drop (r + 1) m

mark :: Board ->  Symbol -> Int -> Int -> Board
mark (Bd bd) sym col row = Bd (mark' bd sym (col, row))

checkVec :: Symbol -> [Symbol] -> Bool
checkVec sym x = all (==sym) x

diag :: Board -> [Symbol]
diag (Bd bd) = zipWith (!!) bd [0..]

flip' :: [[Symbol]] -> [[Symbol]]
flip' [] = []
flip' (bd:bds) = reverse bd : flip' bds

flip'' :: Board -> Board
flip'' (Bd bd) = Bd (flip' bd)

check' :: Symbol -> [[Symbol]] -> Bool
check' sym [] = False
check' sym (bd:bds) = checkVec sym bd || check' sym bds

check :: Symbol -> Board -> Bool
check sym bd = rowCheck || colCheck || diagCheck || flipDiagCheck
    where
        uBd = unBoard bd
        rowCheck = (check' sym uBd)
        colCheck = (check' sym (transpose uBd))
        diagCheck = (checkVec sym (diag bd))
        flipDiagCheck = (checkVec sym (diag (flip'' bd)))

-- -- game :: [Step] -> Int -> Symbol
-- game [] n = E
-- game (x:xs) n = (mark bd x) : game xs n
--     where
--         bd = plain E n


