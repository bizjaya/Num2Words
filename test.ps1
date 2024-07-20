# Endpoint url
$apiUrl = "https://localhost:7040/home/convert"

while ($true) {
    # Ask user for input
    $inputValue = Read-Host -Prompt "Enter your number (or type 'exit' to quit)"

    # Exit
    if ($inputValue -eq 'exit') {
        break
    }

    $inputData = $inputValue

    $jsonData = $inputData | ConvertTo-Json

    try {

        $response = Invoke-RestMethod -Uri $apiUrl -Method Post -Body $jsonData -ContentType "application/json"
        
        # Result
        Write-Output $response
    } catch {
        Write-Output "An error occurred: $_"
    }
}
