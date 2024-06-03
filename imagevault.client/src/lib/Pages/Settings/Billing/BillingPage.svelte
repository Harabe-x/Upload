<script>
     import Card from "../../../Controls/Cards/Card.svelte";
     import {CardCvc, CardExpiry, CardNumber, Elements} from "svelte-stripe";
     import {Chance} from "chance"; 
     import {onMount} from "svelte";

     const chance = new Chance();


     // Dummy data
     const invoices = [
         {invoiceNo : chance.integer({min: 1000, max:100000}), amount : chance.integer({min: 100, max:5000}), description : "Product usages", status : "Pending", generatedOn: chance.date({string: true}), PaidOn: '-'},
         {invoiceNo : chance.integer({min: 1000, max:100000}), amount : chance.integer({min: 100, max:5000}), description : "Product usages", status : "Pending", generatedOn: chance.date({string: true}), PaidOn: '-'},
         {invoiceNo : chance.integer({min: 1000, max:100000}), amount : chance.integer({min: 100, max:5000}), description : "Product usages", status : "Pending", generatedOn: chance.date({string: true}), PaidOn: '-'},
         {invoiceNo : chance.integer({min: 1000, max:100000}), amount : chance.integer({min: 100, max:5000}), description : "Product usages", status : "Paid", generatedOn: chance.date({string: true}), PaidOn: chance.date({string: true})},
         {invoiceNo : chance.integer({min: 1000, max:100000}), amount : chance.integer({min: 100, max:5000}), description : "Product usages", status : "Paid", generatedOn: chance.date({string: true}), PaidOn: chance.date({string: true})},
         {invoiceNo : chance.integer({min: 1000, max:100000}), amount : chance.integer({min: 100, max:5000}), description : "Product usages", status : "Paid", generatedOn: chance.date({string: true}), PaidOn: chance.date({string: true})},
         {invoiceNo : chance.integer({min: 1000, max:100000}), amount : chance.integer({min: 100, max:5000}), description : "Product usages", status : "Paid", generatedOn: chance.date({string: true}), PaidOn: chance.date({string: true})},
         {invoiceNo : chance.integer({min: 1000, max:100000}), amount : chance.integer({min: 100, max:5000}), description : "Product usages", status : "Paid", generatedOn: chance.date({string: true}), PaidOn: chance.date({string: true})},
         {invoiceNo : chance.integer({min: 1000, max:100000}), amount : chance.integer({min: 100, max:5000}), description : "Product usages", status : "Paid", generatedOn: chance.date({string: true}), PaidOn: chance.date({string: true})},


     ];




</script>




<Card title="Payment Method">
    <div class="grid grid-cols-1">
           <div class="flex items-center justify-center h-96">
                <h1> TODO  </h1>
           </div>
    </div>
</Card>



<Card title="Billing History">
    <div class="overflow-x-auto w-full">
        <table class="table w-full" >
            <thead>
            <tr>
                <th>Invoice No</th>
                <th>Invoice Generated On</th>
                <th>Description</th>
                <th>Amount</th>
                <th>Status</th>
                <th>Invoice Paid On</th>
            </tr>
            </thead>
            <tbody>
            {#each invoices as invoice}
                <tr>
                    <td> {invoice.invoiceNo} </td>
                    <td> {invoice.generatedOn} </td>
                    <td> {invoice.description} </td>
                    <td> {invoice.amount} $ </td>
                    <td>
                            <div class=" badge badge-primary" class:badge-success={invoice.status === 'Paid'} >{invoice.status}</div>
                    </td>
                    <td> {invoice.PaidOn} </td>
                <tr>            {/each}
            </tbody>
        </table>
    </div>
</Card>
