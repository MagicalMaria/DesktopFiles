$verify = Test-Path $('filesystem::\\cognizantonline.sharepoint.com@SSL\DavWWWRoot\teams\ITSoftRepo\Shared Documents\EPTAgents')
if($verify)
{
write-host "1"
}
Else
{
write-host "0"
}