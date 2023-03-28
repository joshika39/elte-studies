while ($true) {
    
    switch (Read-Host -Prompt "Kerek egy szamot (0, 1, 2): ") {
        0 {
            Write-Output "A Mai datum: $(Get-Date)"
        }
        1 {
            if (Test-Path .\gyumi.txt) {
                Write-Output "A gyumi.txt tartalma: "
                Get-Content .\gyumi.txt
            } else {
                New-Item .\gyumi.txt -ItemType File
                Write-Output "A fajl nem letezett; gyumi.txt fajl letrahozva"
            }
        }
        2 {
            Write-Output "Bye bye in 2"
            Start-Sleep -Seconds 1
            Write-Output "Bye bye in 1"
            Start-Sleep -Seconds 1
            Exit 0
        }
        default {
            Write-Output "Nincs ilyen menupont!"
        }
    }
}
