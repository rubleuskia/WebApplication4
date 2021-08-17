import { Outlet } from "react-router-dom";

const Layout = () => {
    return (
        <>
            <section className="py-5 text-center container">
                <div className="row py-lg-5">
                    <div className="col-lg-6 col-md-8 mx-auto">
                        <h1 className="fw-light">Accounting solution</h1>
                        <p className="lead text-muted">Create, manage your accounts here</p>
                    </div>
                </div>
            </section>
            <Outlet />
        </>
    );
}

export default Layout;