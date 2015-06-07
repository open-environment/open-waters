<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:decimal-format name="num" decimal-separator="." grouping-separator=","/>
  <xsl:template match="ProcessingReport">
    <html>
      <body>
        <h2 align="center">EPA's WQX Processing Report</h2>
        <hr size="2" width="100%" align="center" color="black"/>
        <xsl:apply-templates select="TransactionIdentifier"/>
        <h2> </h2>
        <xsl:apply-templates select="Status"/>
        <h2> </h2>
        <hr></hr>
        <h1> </h1>
        <h2> </h2>
        <xsl:apply-templates select="Counts"/>
        <h1> </h1>
        <h2> </h2>
        <hr></hr>
        <p style="page-break-before: always" />
        <xsl:apply-templates select="ProcessingFailures"/>
        <h1> </h1>
        <h2> </h2>
        <hr></hr>
        <p style="page-break-before: always" />
        <xsl:apply-templates select="Log"/>
        <h1> </h1>
        <h2> </h2>
      </body>
    </html>
  </xsl:template>
  <xsl:template match="TransactionIdentifier">
    <h2 align="left">
      <b>Transaction ID: </b>
      <xsl:value-of select="../TransactionIdentifier"/>
    </h2>
  </xsl:template>

  <xsl:template match="Status">
    <h2 align="left">
      <b>Status: </b>
      <xsl:value-of select="../Status"/>
    </h2>
  </xsl:template>



  <xsl:template match="Counts">
    <h2 align="left">
      <b>Summary Information</b>
    </h2>

    <table>
      <tr>
        <td align="left">
          <font face="Arial" size="3">
            # Errors:
          </font>
        </td>
        <td align="left">
          <font face="Arial" size="3">
            <xsl:value-of select="format-number(Error, '###,###,##0','num')"/>
          </font>
        </td>
      </tr>
      <tr>
        <td align="left">
          <font face="Arial" size="3">
            # Warnings:
          </font>
        </td>
        <td align="left">
          <font face="Arial" size="3">
            <xsl:value-of select="format-number(Warning, '###,###,##0','num')"/>
          </font>
        </td>
      </tr>
    </table>
  </xsl:template>


  <xsl:template match="ProcessingFailures">
    <h2 align="left">
      <b>Failed Records</b>
    </h2>
    <table>
      <xsl:for-each select="ProjectIdentifier">
        <tr>
          <td align="left">
            <font face="Arial" size="2">
               Project: <xsl:value-of select="."/>
            </font>
          </td>
        </tr>
      </xsl:for-each>
      <xsl:for-each select="MonitoringLocationIdentifier">
        <tr>
          <td align="left">
            <font face="Arial" size="2">
              Monitoring Location: <xsl:value-of select="."/>
            </font>
          </td>
        </tr>
      </xsl:for-each>

      <xsl:for-each select="ActivityIdentifier">
        <tr>
          <td align="left">
            <font face="Arial" size="2">
              <b>Activity ID:</b> <xsl:value-of select="."/>
            </font>
          </td>
        </tr>
      </xsl:for-each>
    </table>


  </xsl:template>

  <xsl:template match="Log">
    <h2 align="left">
      <b>Processing Log </b>
    </h2>
    <table bgcolor="white" border = "1" bordercolor = "black" cellspacing = "0" cellpadding = "5" width = "100%">
      <tfoot 	class="sec2" valign="bottom">
        <tr>
          <td align="left" Colspan="2">
            <font face="Arial" size="1">*This table shows unique errors. </font>
          </td>
        </tr>
      </tfoot>
      <tbody>
        <xsl:for-each select="LogDetail[not(./Text=preceding::LogDetail/Text)]">
          <tr>

            <xsl:choose>
              <xsl:when test="./Type = 'Message'">

                <td align="left" bgcolor="#CEEFBD">
                  <font face="Arial" size="2">
                    <xsl:value-of select="./Type"/>
                  </font>
                </td>
              </xsl:when>
              <xsl:when test="./Type = 'Error'">

                <td align="left" bgcolor="#FF9473">
                  <font face="Arial" size="2">
                    <xsl:value-of select="./Type"/>
                  </font>
                </td>
              </xsl:when>
              <xsl:when test="./Type = 'Warning'">

                <td align="left" bgcolor= "C6EFF7">
                  <font face="Arial" size="2">
                    <xsl:value-of select="./Type"/>
                  </font>
                </td>
              </xsl:when>
              <xsl:otherwise>

                <td align="left">
                  <font face="Arial" size="2">
                    <xsl:value-of select="./Type"/>
                  </font>
                </td>
              </xsl:otherwise>
            </xsl:choose>
            <td align="left">
              <font face="Arial" size="2">
                <xsl:value-of select="./Text"/>
              </font>
            </td>

          </tr>
        </xsl:for-each>
      </tbody>
    </table>

  </xsl:template>

</xsl:stylesheet>
