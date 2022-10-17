# BandCheck

<b>BandCheck</b> is a really simple network bandwidth check tool, working on Linux, macOS and Windows.

## Requirements
[.NET 6 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

## Usage
Run the same `BandCheck.exe` on one of the endpoint (a server, a client or other), to put it in listen mode, then run `BandCheck.exe` on another endpoint on the same network to test the available bandwidth.

### Quickest mode

On the first endpoint
> Bandcheck.exe -s

On the second endpoint (replace IPV4 address with first endpoint address or name if resolved)
> Bandcheck.exe -t 192.168.1.1
