<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" encoding="UTF-8" indent="yes"/>

  <xsl:template match="/">
    <html>
      <head>
        <title>Software Catalog</title>
        <style>
          table { width: 100%; border-collapse: collapse; }
          th, td { padding: 8px; text-align: left; border-bottom: 1px solid #ddd; }
          th { background-color: #f2f2f2; }
        </style>
      </head>
      <body>
        <h2>Software Catalog</h2>
        <xsl:for-each select="FacultyNetwork/SoftwareCategory">
          <h3>
            <xsl:value-of select="@name"/>
            (<xsl:for-each select="@*">
              <xsl:if test="position() > 1">, </xsl:if>
              <xsl:value-of select="."/>
            </xsl:for-each>)
          </h3>
          <table>
            <tr>
              <th>Name</th>
              <th>Description</th>
              <th>Type</th>
              <th>Version</th>
              <th>Author</th>
              <th>Terms</th>
              <th>Distribution</th>
            </tr>
            <xsl:for-each select="Software">
              <tr>
                <td>
                  <xsl:value-of select="Name"/>
                </td>
                <td>
                  <xsl:value-of select="Description"/>
                </td>
                <td>
                  <xsl:value-of select="Type"/>
                </td>
                <td>
                  <xsl:value-of select="Version"/>
                </td>
                <td>
                  <xsl:value-of select="Author"/>
                </td>
                <td>
                  <xsl:value-of select="Terms"/>
                </td>
                <td>
                  <xsl:value-of select="Distribution"/>
                </td>
              </tr>
            </xsl:for-each>
          </table>
        </xsl:for-each>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
