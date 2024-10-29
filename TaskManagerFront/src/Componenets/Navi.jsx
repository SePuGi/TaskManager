import { useContext } from "react";
import { NavLink } from "react-router-dom";
import ContextLogin from "../Controllers/ContextLogin";

function Navi() {
    const { user } = useContext(ContextLogin);
    return (
        <>
            <nav>
                <h1>{user.nombre}</h1>
                <NavLink to="./">Home</NavLink>

            </nav>
        </>
    )
}

export default Navi;