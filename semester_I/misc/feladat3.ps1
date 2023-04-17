[float] $a = Read-Host -Prompt "Kerem az 'a' oldal erteket: " 
[float] $b = Read-Host -Prompt "Kerem az 'b' oldal erteket: " 
[float] $c = Read-Host -Prompt "Kerem az 'c' oldal erteket: " 

if ((($a + $b) -gt $c) -and `
    (($a + $c) -gt $b) -and `
    (($b + $c) -gt $a)) {
    $s = ($a + $b + $c) / 2
    $terulet = [Math]::Sqrt($s * ($s - $a) * ($s - $b) * ($s - $c))
    $kerulet = $a + $b + $c
    Write-Output "Terulet: $terulet"
    Write-Output "Kerulet: $kerulet"
}
else {
    Write-Output "Roszz ertekeket adott meg"
}
