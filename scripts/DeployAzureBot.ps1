$ResourceGroup = 'ResourceGroup1'
$BotTemplateFile = 'deployBotService.bicep'
$AzureOpenAITemplateFile = 'deployAzureOpenAI.bicep'

# Login to Azure
az login

# Deploy the Azure Bot Service
$deploymentResult = az deployment group create `
    --resource-group $ResourceGroup `
    --template-file $BotTemplateFile `
    --only-show-errors `
    --output json | ConvertFrom-Json

$appId = $deploymentResult.properties.outputs.appId.value
$tenantId = $deploymentResult.properties.outputs.tenantId.value
$botName = $deploymentResult.properties.outputs.botApp.value
  
# Generate a new client secret for the returned App ID
$deploymentSecretResult = az ad app credential reset --id $appId --output json | ConvertFrom-Json
$secret = $deploymentSecretResult.password

# Deploy the Azure OpenAI service
$deploymentOpenAIResult = az deployment group create `
    --resource-group $ResourceGroup `
    --template-file $AzureOpenAITemplateFile `
    --parameters resourceBaseName=$botName `
    --only-show-errors `
    --output json | ConvertFrom-Json
  
$azureOpenAIEndpoint = $deploymentOpenAIResult.properties.outputs.AZURE_OPENAI_ENDPOINT.value
$azureOpenAIApiKey = $deploymentOpenAIResult.properties.outputs.SECRET_AZURE_OPENAI_API_KEY.value

Write-Host("================================================")
Write-Host ("App Id: $appId")
Write-Host ("Tenant Id: $tenantId")
Write-Host ("Secret: $secret")
Write-Host ("Azure OpenAI Endpoint: $azureOpenAIEndpoint")
Write-Host ("Azure OpenAI ApiKey: $azureOpenAIApiKey")
Write-Host ("Azure OpenAI deployment name: gpt-4o-mini")
Write-Host("================================================")