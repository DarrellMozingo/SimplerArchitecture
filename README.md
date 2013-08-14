Got tired of the repository pattern and related common abstractions, so borrowed some concpets from Ayende and used a setup similar to this on another project with good success so far. Wanted to put a stripped down version online to show others.

This setup relies more heavily on controller down/subcutaneous tests. In this example it's against a local MSSQL database rather than an in-memory one just to keep parity with most production setups (SQLite still has some weirdness around dates, schemas, lack of sprocs, etc).

The big up side is direct access to the NHibernate ISession so more control over what SQL is used while still allowing fakes for other pieces of the system to be injected in. I've also used this concept for email and message bus abstractions (having easy access to them in the controller but still easily faked out in the tests).
