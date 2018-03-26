// https://reactjs.net/

var i = 0;

console.log(i++);

import ShoppingList from "ShoppingList";

console.log(i++);
// Create a function to wrap up your component
function App() {
    return (
        <div>
            <ShoppingList name="@luispagarcia on Dev.to!" />
        </div>
    )
}

console.log(i++);
// Use the ReactDOM.render to show your component on the browser
ReactDOM.render(
    <App />,
    document.getElementById('jsxcontent')
)
