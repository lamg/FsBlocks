module FsBlocks.Algorand.QR

open System
open Net.Codecrete.QrCodeGenerator

type AlgorandUrl =
  { address: string
    amount: uint64 option
    label: string option
    asset: uint64 option
    note: string option }

let buildUrl (scheme: string) (host: string) (query: (string * string) list) =
  let builder = UriBuilder(scheme, host, -1)

  let queryString =
    query
    |> List.map (fun (k, v) -> $"%s{Uri.EscapeDataString k}=%s{Uri.EscapeDataString v}")
    |> String.concat "&"

  builder.Query <- queryString
  builder.Uri.ToString()

let algorandUrlToString (u: AlgorandUrl) =
  let optToStr = Option.map (_.ToString())

  let queryParams =
    [ "amount", u.amount |> optToStr
      "label", u.label
      "asset", u.asset |> optToStr
      "note", u.note ]
    |> List.choose (function
      | q, Some v -> Some(q, v)
      | _ -> None)

  buildUrl "algorand" u.address queryParams

let algorandUrlToSvgQR (u: AlgorandUrl) =
  let url = u |> algorandUrlToString
  let qr = QrCode.EncodeText(url, QrCode.Ecc.Medium)
  qr.ToSvgString 4
