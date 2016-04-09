Param($url, $privateKey)
# get the directory of this script file
$testsDirectory = [IO.Path]::GetDirectoryName($MyInvocation.MyCommand.Path) + '\SecurionPayTests'
# get the full path and file name of the App.config file in the same directory as this script
$appConfigFile = [IO.Path]::Combine($testsDirectory, 'App.config')

$doc = New-Object System.Xml.XmlDocument
$doc.Load($appConfigFile)
$urlNode = $doc.SelectSingleNode('configuration/appSettings/add[@key="gateway_test_url"]')
$urlNode.Attributes['value'].Value = $url
$keyNode = $doc.SelectSingleNode('configuration/appSettings/add[@key="gateway_test_key"]')
$keyNode.Attributes['value'].Value = $privateKey
$doc.Save($appConfigFile)