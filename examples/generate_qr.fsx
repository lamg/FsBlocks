#r "nuget: FsBlocks, 0.0.1"

open System.IO
open System.Text
open FsBlocks.Algorand.QR

let supportThisProject =
  { address = "UJ6N6ZWLS4MH5324C5J6TAWRXXXVJNS7HVURGLEZ3QRV7ONGIV5ZZSUGWI"
    amount = Some 100_000_000UL
    label = None
    asset = None
    note = Some "FsBlocks support" }
  |> algorandUrlToSvgQR

File.WriteAllText("support.svg", supportThisProject, Encoding.UTF8)
