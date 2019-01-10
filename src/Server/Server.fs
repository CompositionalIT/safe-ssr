open System.IO
open System.Threading.Tasks

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open FSharp.Control.Tasks.V2
open Giraffe
open Saturn
open Shared
open Fable.Helpers.React
open Fable.Helpers.React.Props


let publicPath = Path.GetFullPath "../Client/public"
let port = 8085us

let rand = System.Random()

let initialModel () = { Counter = Some { Value = rand.Next(0, 50) } }

let makeInitialHtml model =
    html [ ] [
        head [ ] [
            title [ ] [ str "SAFE Template" ]
            meta [ CharSet "utf-8" ]
            meta [ Name "viewport"; HTMLAttr.Content "width=device-width, initial-scale=1" ]
            link [ Rel "stylesheet"; Href "https://cdnjs.cloudflare.com/ajax/libs/bulma/0.7.1/css/bulma.min.css" ]
            link [
              Rel "stylesheet"
              Href "https://use.fontawesome.com/releases/v5.6.1/css/all.css"
              Integrity "sha384-gfdkjb5BdAXd+lj+gudLWI+BXq4IuLW5IT+brZEZsLFm++aCMlF1V92rMkPaX4PP"
              CrossOrigin "anonymous" ]
            link [ Href "https://fonts.googleapis.com/css?family=Open+Sans"; Rel "stylesheet" ]
            link [ Rel "shortcut icon"; Type "image/png"; Href "/favicon.png" ] ]
        body [ ] [
            div [ Id "elmish-app" ] [ view model ignore ]
            script [ ] [ RawText (sprintf "var __INIT_MODEL__ = %s" (Thoth.Json.Net.Encode.Auto.toString(0, model))) ]
            script [ Src "./bundle.js" ] [ ] ] ]

let webApp = router {
    get "/" (fun next ctx ->
        task {
            let html = initialModel () |> makeInitialHtml |> Fable.Helpers.ReactServer.renderToString
            return! htmlString html next ctx
        })
}

let configureSerialization (services:IServiceCollection) =
    services.AddSingleton<Giraffe.Serialization.Json.IJsonSerializer>(Thoth.Json.Giraffe.ThothSerializer())

let app = application {
    url ("http://0.0.0.0:" + port.ToString() + "/")
    use_router webApp
    memory_cache
    use_static publicPath
    service_config configureSerialization
    use_gzip
}

run app
