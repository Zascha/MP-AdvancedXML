<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:x="http://library.by/catalog"
                exclude-result-prefixes="x">

  <xsl:param name ="Title" select="''"/>
  <xsl:param name ="Link" select="''"/>
  <xsl:param name ="Description" select="''"/>

  <xsl:template match="@* | node()">
    <xsl:element name="rss">
      <xsl:element name="title">
        <xsl:value-of select="$Title"/>
      </xsl:element>
      <xsl:element name="link">
        <xsl:value-of select="$Link"/>
      </xsl:element>
      <xsl:element name="description">
        <xsl:value-of select="$Description"/>
      </xsl:element>
      <xsl:for-each select="x:book">
        <xsl:element name="item">
          <xsl:element name="title">
            <xsl:value-of select="x:title"/>
          </xsl:element>
          <xsl:element name="link">
            <xsl:variable name="isbn-link">
              <xsl:choose>
                <xsl:when test="x:isbn!=''" >
                  <xsl:value-of select="concat('http://my.safaribooksonline.com/', x:isbn)"/>
                </xsl:when>
                <xsl:otherwise>
                  <xsl:value-of select="''"/>
                </xsl:otherwise>
              </xsl:choose>
            </xsl:variable>            
            <xsl:value-of select="$isbn-link"/>
          </xsl:element>
          <xsl:element name="description">
            <xsl:value-of select="x:description"/>
          </xsl:element>
          <xsl:element name="author">
            <xsl:value-of select="x:author"/>
          </xsl:element>
          <xsl:element name="pubDate">
            <xsl:value-of select="x:publish_date"/>
          </xsl:element>
        </xsl:element>
      </xsl:for-each>
    </xsl:element>
  </xsl:template>
</xsl:stylesheet>
