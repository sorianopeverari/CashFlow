# Cash Flow

It's a PoC for a Solutions Architecture challenge.

# Specification Requirements

## Functional Requirements

* **FR-01 - Credit transaction**

  * **FR-01.01 - Enter amount**
    - Seller will enter a amount. It MUST BE positive decimal value.

* **FR-02 - Debit transaction**

  * **FR-02.01 - Enter amount**
    - Seller will enter a amount. It MUST BE positive decimal value.

  * **FR-02.03 - Convert negative value**
    - System will MUST convert amount value to a negative value.

  * **FR-02.04 - Ensuring the daily balance**
    - System will MUST ensure that the sum of daily transactions is greater than zero.
    - System will MUST DENY debit transactions that don't ensures these requirements.
    
 * **FR-03 - Extract Daily Balance**
    - System MUST extracts the daily balances to load in a Data Warehouse at a configured time.
    
## Non-Functional Requirements

 * **NFR-01 - Date**
 
   * **NFR-01.01 - Date time zone**
   - Date information MUST BE stored and interoperable in the UTC time zone. The UI layer will be responsible for the conversion.
 
   * **NFR-01.02 - Current time refenrece**
   - Date information MUST BE generated from the business layer when a current time is required.
