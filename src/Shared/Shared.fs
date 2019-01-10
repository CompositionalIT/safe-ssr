module Shared

open Elmish
open Elmish.React
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fable.PowerPack.Fetch
open Thoth.Json
open Fulma

type Counter = { Value : int }

type Model = { Counter: Counter option }

type Msg =
| Increment
| Decrement
| InitialCountLoaded of Result<Counter, exn>

let initialModel () = { Counter = Some { Value = 42 } }

// defines the initial state and initial command (= side-effect) of the application
let init () : Model * Cmd<Msg> =
    initialModel(), Cmd.none

let safeComponents =
    let components =
        span [ ]
           [
             a [ Href "https://saturnframework.github.io" ] [ str "Saturn" ]
             str ", "
             a [ Href "http://fable.io" ] [ str "Fable" ]
             str ", "
             a [ Href "https://elmish.github.io/elmish/" ] [ str "Elmish" ]
             str ", "
             a [ Href "https://mangelmaxime.github.io/Fulma" ] [ str "Fulma" ]
           ]

    p [ ]
        [ strong [] [ str "SAFE Template" ]
          str " powered by: "
          components ]

let show = function
| { Counter = Some counter } -> string counter.Value
| { Counter = None   } -> "Loading..."

let button txt onClick =
    Button.button
        [ Button.IsFullWidth
          Button.Color IsPrimary
          Button.OnClick onClick ]
        [ str txt ]

let view (model : Model) (dispatch : Msg -> unit) =
    div []
        [ Navbar.navbar [ Navbar.Color IsPrimary ]
            [ Navbar.Item.div [ ]
                [ Heading.h2 [ ]
                    [ str "SAFE Template" ] ] ]

          Container.container []
              [ Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                    [ Heading.h3 [] [ str ("Press buttons to manipulate counter: " + show model) ] ]
                Columns.columns []
                    [ Column.column [] [ button "-" (fun _ -> dispatch Decrement) ]
                      Column.column [] [ button "+" (fun _ -> dispatch Increment) ] ] ]

          Footer.footer [ ]
                [ Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                    [ safeComponents ] ] ]

let makeInitialHtml initialModel =
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
            div [ Id "elmish-app" ] [
                view initialModel ignore
            ]
            script [ Src "./bundle.js" ] [ ] ] ]

