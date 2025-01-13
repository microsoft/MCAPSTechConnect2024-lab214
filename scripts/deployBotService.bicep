extension microsoftGraphV1

@description('Location into which the Bot will be deployed.')
param location string = 'global'

@description('Fixed prefix for resource names.')
param namePrefix string = 'MCAPS-TS-'

@description('Random suffix for resource names.')
param randomSuffix string = newGuid()

/*
  Generate a unique suffix from the resource group ID to ensure names don't collide.
  The suffix length is up to you—here we take the first 8 characters from a uniqueString.
*/

var shortSuffix = toLower(take(randomSuffix, 8))

@description('Complete resource name for both Bot Service and App Registration.')
var botAppName = '${namePrefix}${shortSuffix}'

// -------------------------------------------------------------------------------------
// 1) Create a single-tenant App Registration in Microsoft Entra (Azure AD) via MS Graph
// -------------------------------------------------------------------------------------
resource botAppReg 'Microsoft.Graph/applications@v1.0' = {
  displayName: botAppName
  signInAudience: 'AzureADMyOrg'
  uniqueName: botAppName
}

// -------------------------------------------------------------------------------------
// 3) Create the Azure Bot Service resource
//    Link it to the newly created single-tenant App Registration
// -------------------------------------------------------------------------------------
resource botService 'Microsoft.BotService/botServices@2023-09-15-preview' = {
  name: botAppName
  location: location
  kind: 'bot'
  sku: {
    name: 'F0'
  }
  properties: {
    displayName: botAppName
    // Use the newly generated App Registration’s appId
    msaAppId: botAppReg.appId
    // Required to mark it as single tenant
    msaAppTenantId: tenant().tenantId
    msaAppType: 'SingleTenant'
  }
}

// -------------------------------------------------------------------------------------
// 4) Output relevant values
// -------------------------------------------------------------------------------------
@description('App (Client) ID for the newly created Microsoft Entra App Registration')
output appId string = botAppReg.appId

@description('Tenant ID where the app was created')
output tenantId string = tenant().tenantId

@description('The name of the newly created Azure Bot Service')
output botApp string = botAppName
