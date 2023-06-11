import React from "react";
import ReactDOM from "react-dom/client";
import { Provider } from "react-redux";
import { BrowserRouter } from "react-router-dom";
import { ToastContainer } from "react-toastify";

import { GoogleOAuthProvider } from "@react-oauth/google";

import App from "./App";
import reportWebVitals from "./reportWebVitals";
import { store } from "./store";

import "react-toastify/dist/ReactToastify.css";
import "./index.css";

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <GoogleOAuthProvider clientId={process.env.REACT_APP_CLIENT_ID!}>
    <React.StrictMode>
      <BrowserRouter>
        <Provider store={store}>
          <App />
          <ToastContainer
            position="bottom-right"
            autoClose={5000}
            hideProgressBar
          />
        </Provider>
      </BrowserRouter>
    </React.StrictMode>
  </GoogleOAuthProvider>
);

reportWebVitals();
