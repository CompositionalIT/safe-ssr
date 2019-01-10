#r "netstandard"
#load "../../.paket/load/netcoreapp2.1/Server/server.group.fsx"

open Fable.Helpers
open Fable.Helpers.React
open Fable.Helpers.React.Props

html [ ]
    [ head [ ]
        [ title [ ]
            [ str "SAFE Template" ]
          meta [ CharSet "utf-8" ]
          meta [ Name "viewport"
                 HTMLAttr.Content "width=device-width, initial-scale=1" ]
          link [ Rel "stylesheet"
                 Href "https://cdnjs.cloudflare.com/ajax/libs/bulma/0.7.1/css/bulma.min.css" ]
          link [ Rel "stylesheet"
                 Href "https://use.fontawesome.com/releases/v5.6.1/css/all.css"
                 Integrity "sha384-gfdkjb5BdAXd+lj+gudLWI+BXq4IuLW5IT+brZEZsLFm++aCMlF1V92rMkPaX4PP"
                 CrossOrigin "anonymous" ]
          link [ Href "https://fonts.googleapis.com/css?family=Open+Sans"
                 Rel "stylesheet" ]
          link [ Rel "shortcut icon"
                 Type "image/png"
                 Href "/favicon.png" ] ]
      body [ ]
        [ div [ Id "elmish-app" ]
            [ ] ] ]
|> ReactServer.renderToString
