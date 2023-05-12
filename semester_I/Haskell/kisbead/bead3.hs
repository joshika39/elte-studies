optSquare :: Int -> Bool -> Int
optSquare x True = x*x
optSquare x False = x

mult 1 0 = 0
mult 1 1 = 1
mult 1 2 = 2
mult 2 1 = 2
mult 2 2 = 4
mult 2 0 = 0
mult 0 1 = 0
mult 0 2 = 0
mult 0 0 = 0
mult x y = error "Valamely szam nincs a halmazban"


trd (_, _, z) = z

pairOfPairs x y z w = ((x, y), (z, w))

mergePairs ((x, y), (z, w)) = (x, y, z, w)

cons x (a, b, c, d) = (x, a, b, c, d)

nth :: (a,a,a,a,a) -> Int -> a
nth (a,_,_,_,_) 0 = a
nth (_,a,_,_,_) 1 = a
nth (_,_,a,_,_) 2 = a
nth (_,_,_,a,_) 3 = a
nth (_,_,_,_,a) 4 = a
