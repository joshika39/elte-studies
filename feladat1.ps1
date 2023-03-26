Write-Output "YQMHWO"
Write-Output "Elso feladat"

foreach ($szam in 0..10 | ForEach-Object {$_ / 10}) {
    $ertek = [math]::Sin($szam)
    Write-Output "$szam $ertek"
}
