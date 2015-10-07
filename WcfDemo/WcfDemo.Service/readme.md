Voor Named Pipe Binding:
1. Features activeren (.NET Framework 4.5 Features --> WCF Services --> Named Pipe Activation)
1. IIS Bindings configureren met in Binding Information een herkenbare naam
1. IIS Advanced Settings net.pipe toevoegen aan Enabled Protocols
1. Service config updaten: named pipe endpoint toevoegen
1. Client config updaten: named pipe endpoint naar herkenbare naam
1. Op service en client de security mode op None zetten