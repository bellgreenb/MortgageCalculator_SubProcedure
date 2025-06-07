Public Class frmMortgageCalculator

    Private Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click
        If cboLoanTerm.SelectedIndex = -1 Then
            MessageBox.Show("Please select a valid loan term")
        End If

        Try
            Dim loanAmount As Decimal = CDec(txtLoanAmount.Text)
            Dim loanTerm As Decimal = 0 'loanTerm initialized as 0 years. 
            Dim yearlyInterestRate As Decimal = CDec(txtYearlyInterestRate.Text)
            Dim monthlyInterestRate As Decimal = yearlyInterestRate / 12 / 100 'convert yearly rate to monthly to apply to monthly payment.
            Dim monthlyPayment As Decimal
            Dim balance As Decimal = loanAmount

            'this if statement evaluates if there is a loan term.
            Select Case cboLoanTerm.SelectedItem.ToString()
                Case "   30-yr fixed"
                    loanTerm = 30
                Case "   20-yr fixed"
                    loanTerm = 20
                Case "   15-yr fixed"
                    loanTerm = 15
            End Select

            'monthly payment calculation algorithm
            Dim totalMonths As Decimal = loanTerm * 12 'convert loanTerm to months for calculation

            'initializing compound factor
            Dim compoundFactor As Decimal = 1

            'calculate compound factor 
            For i As Integer = 1 To CInt(totalMonths)
                compoundFactor *= (1 + monthlyInterestRate)
            Next

            Me.ComputeMonthlyPayment(monthlyInterestRate, loanAmount, compoundFactor, totalMonths, monthlyPayment)

            If monthlyPayment >= 5000 Then
                MessageBox.Show("According to common rules, your monthly mortgage payment" & vbNewLine &
                                "should be no more than 28% of your monthly gross income.")
            End If

            'display monthly payment in txtMonthlyPayment.Text textbox.
            txtMonthlyPayment.Text = FormatCurrency(monthlyPayment)
        Catch ex As InvalidCastException
            MessageBox.Show("Please check entries for valid numeric data.", ex.GetType.ToString)
        Catch ex As OverflowException
            MessageBox.Show("According to common rules, your monthly mortgage payment" & vbNewLine &
                "should be no more than 28% of your monthly gross income." & vbNewLine & ex.StackTrace, ex.GetType.ToString)
        Finally
            txtLoanAmount.Select()
        End Try
    End Sub

    Private Sub frmMortgageCalculator_DoubleClick(sender As Object, e As EventArgs) Handles Me.DoubleClick
        ClearAllTextBoxes()
    End Sub

    Private Sub ClearAllTextBoxes() Handles txtMonthlyPayment.MouseHover
        txtLoanAmount.Text = ""
        Dim loanAmount = 0

        cboLoanTerm.SelectedIndex = -1 ' resets cboLoanTerm selection
        Dim loanTerm = 0
        cboLoanTerm.Text = "Please select a loan Term"

        txtYearlyInterestRate.Text = ""
        Dim yearlyInterestRate = 0

        txtMonthlyPayment.Text = ""
        Dim monthlyPayment = 0
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtLoanAmount.Text = ""
        Dim loanAmount = 0

        cboLoanTerm.SelectedIndex = -1 ' resets cboLoanTerm selection
        Dim loanTerm = 0
        cboLoanTerm.Text = "Please select a loan Term"

        txtYearlyInterestRate.Text = ""
        Dim yearlyInterestRate = 0

        txtMonthlyPayment.Text = ""
        Dim monthlyPayment = 0
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub ComputeMonthlyPayment(monthlyInterestRate As Decimal, loanAmount As Decimal, compoundFactor As Decimal, totalMonths As Decimal, ByRef monthlyPayment As Decimal)
        'calculate monthly payment using the initialized & calculated compound factor
        If monthlyInterestRate > 0 Then
            monthlyPayment = loanAmount * (monthlyInterestRate * compoundFactor) / (compoundFactor - 1)
        Else
            monthlyPayment = loanAmount / totalMonths
        End If
    End Sub


End Class
