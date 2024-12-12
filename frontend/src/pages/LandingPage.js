import React from "react";
import { Link } from "react-router-dom";
import "./LandingPage.css";

const LandingPage = () => {
  return (
    <div className="landing-container">
      <h1>Admin Dashboard</h1>
      <p>Manage Products and Categories easily.</p>
      <div className="button-group">
        <Link to="/login">
          <button className="primary-btn">Login</button>
        </Link>
        <Link to="/register">
          <button className="secondary-btn">Register</button>
        </Link>
      </div>
    </div>
  );
};

export default LandingPage;
